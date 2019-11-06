import { Component, OnInit, ViewChild, OnDestroy } from '@angular/core';
import { MatPaginator, MatSort, MatDialog, MatSnackBar } from '@angular/material';
import { Infirmary } from '../models/infirmary';
import { untilComponentDestroyed } from '@w11k/ngx-componentdestroyed';
import { InfirmaryService } from '../services/infirmary.service';
import { ConfirmComponent } from 'src/app/shared/dialogs/confirm/confirm.component';
import * as XLSX from 'xlsx';
import { ServerSideListDataSource } from 'src/app/shared/generics/server-side-list-datasource';

@Component({
  selector: 'app-infirmaries-list',
  templateUrl: './infirmaries-list.component.html',
  styleUrls: ['./infirmaries-list.component.scss']
})
export class InfirmariesListComponent implements OnInit, OnDestroy {
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  dataSource: ServerSideListDataSource<Infirmary>;

  /** Columns displayed in the table. Columns IDs can be added, removed, or reordered. */
  displayedColumns = ['animal.name', 'admissionDate', 'diagnosis', 'actions'];

  /**
   *
   */
  constructor(
    private dialog: MatDialog,
    private snackBar: MatSnackBar,
    private infirmaryService: InfirmaryService
  ) { }

  ngOnInit() {
    this.dataSource = new ServerSideListDataSource(this.paginator, this.sort);
    this.dataSource.setDataSource((params: {[x: string]: any}) => this.infirmaryService.getAllWithRelations(params));
    this.refresh();
  }

  refresh() {
    this.dataSource.updateData();
  }

  delete(infirmary: Infirmary) {
    const dialogRef = this.dialog.open(ConfirmComponent, { width: '250px', data: { content: 'Are you sure you want to delete it?' } });
    dialogRef.afterClosed().pipe(untilComponentDestroyed(this)).subscribe(result => {
      if (result) {
        this.infirmaryService.delete(infirmary.id).pipe(untilComponentDestroyed(this)).subscribe(() => {
          this.refresh();
          this.snackBar.open(`Infirmary "${infirmary.admissionDate}" has been deleted.`, 'Dismiss', { duration: 3000 });
        });
      }
    });
  }

  export() {
    const data = this.dataSource.getData().map(u => {
      return {
        Animal: u.animal.name,
        'Admision date': new Date(u.admissionDate),
        Diagnosis: u.diagnosis
      };
    });
    const workSheet: XLSX.WorkSheet = XLSX.utils.json_to_sheet(data);
    const workBook: XLSX.WorkBook = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(workBook, workSheet, 'Visits');
    XLSX.writeFile(workBook, 'infirmary.xlsx', { bookType: 'xlsx', type: 'buffer' });
  }

  ngOnDestroy(): void { }
}
