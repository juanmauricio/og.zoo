import { DataSource } from '@angular/cdk/collections';
import { MatPaginator, MatSort } from '@angular/material';
import { tap, finalize } from 'rxjs/operators';
import { Observable, merge, BehaviorSubject, Subscription } from 'rxjs';
import { Paginated } from './paginated';

/**
 * Data source for the UsersList view. This class should
 * encapsulate all logic for fetching and manipulating the displayed data
 * (including sorting, pagination, and filtering).
 */
export class ServerSideListDataSource<TEntity> extends DataSource<TEntity> {
  private dataSource: (params: { [x: string]: any; }) => Observable<Paginated<TEntity>>;
  private changesSub: Subscription = new Subscription();
  private itemsSubject = new BehaviorSubject<TEntity[]>([]);
  private loadingSubject = new BehaviorSubject<boolean>(false);
  loading$ = this.loadingSubject.asObservable();

  constructor(private paginator: MatPaginator, private sort: MatSort) {
    super();
  }

  setDataSource(getPaginated: (params: { [x: string]: any; }) => Observable<Paginated<TEntity>>): void {
    this.dataSource = getPaginated;
    this.changesSub.unsubscribe();
    this.changesSub = merge(this.sort.sortChange, this.paginator.page)
      .pipe(
        tap(() => this.updateData())
      )
      .subscribe();
    this.updateData();
  }

  /**
   * Update the data of items subject
   *
   */
  updateData() {
    this.loadingSubject.next(true);
    const pageSize = this.paginator.pageSize || 5;
    this.dataSource(
      {
        pageIndex: this.paginator.pageIndex,
        direction: this.sort.direction,
        sortBy: this.sort.active,
        pageSize
      })
      .pipe(
        finalize(() => this.loadingSubject.next(false))
      )
      .subscribe(page => {
        this.paginator.length = page.totalItems;
        this.itemsSubject.next(page.items);
      });
  }

  getData(): TEntity[] {
    return this.itemsSubject.getValue();
  }

  /**
   * Connect this data source to the table. The table will only update when
   * the returned stream emits new items.
   * @returns A stream of the items to be rendered.
   */
  connect(): Observable<TEntity[]> {
    // Combine everything that affects the rendered data into one update
    // stream for the data-table to consume.
    return this.itemsSubject.asObservable();
  }

  /**
   *  Called when the table is being destroyed. Use this function, to clean up
   * any open connections or free any held resources that were set up during connect.
   */
  disconnect() {
    this.itemsSubject.complete();
    this.loadingSubject.complete();
    this.changesSub.unsubscribe();
  }
}
