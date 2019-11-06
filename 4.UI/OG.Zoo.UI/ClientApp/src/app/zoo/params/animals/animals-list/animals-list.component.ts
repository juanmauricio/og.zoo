import { Component, OnInit, ViewChild, OnDestroy } from '@angular/core';
import { MatPaginator, MatSort, MatDialog, MatSnackBar } from '@angular/material';
import { Animal } from '../models/animal';
import { untilComponentDestroyed } from '@w11k/ngx-componentdestroyed';
import { AnimalService } from '../services/animal.service';
import { ConfirmComponent } from 'src/app/shared/dialogs/confirm/confirm.component';
import * as XLSX from 'xlsx';
import { ServerSideListDataSource } from 'src/app/shared/generics/server-side-list-datasource';

@Component({
  selector: 'app-animals-list',
  templateUrl: './animals-list.component.html',
  styleUrls: ['./animals-list.component.scss']
})
export class AnimalsListComponent implements OnInit, OnDestroy {
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  dataSource: ServerSideListDataSource<Animal>;

  /** Columns displayed in the table. Columns IDs can be added, removed, or reordered. */
  displayedColumns = ['name', 'age', 'country', 'species', 'subspecies', 'eatingHabits', 'type', 'actions'];

  /**
   *
   */
  constructor(
    private dialog: MatDialog,
    private snackBar: MatSnackBar,
    private animalService: AnimalService
  ) { }

  ngOnInit() {
    this.dataSource = new ServerSideListDataSource(this.paginator, this.sort);
    this.dataSource.setDataSource((params: {[x: string]: any}) => this.animalService.getPaginated(params));
  }

  refresh() {
    this.dataSource.updateData();
  }

  delete(animal: Animal) {
    const dialogRef = this.dialog.open(ConfirmComponent, { width: '250px', data: { content: 'Are you sure you want to delete it?' } });
    dialogRef.afterClosed().pipe(untilComponentDestroyed(this)).subscribe(result => {
      if (result) {
        this.animalService.delete(animal.id).pipe(untilComponentDestroyed(this)).subscribe(() => {
          this.dataSource.updateData();
          this.snackBar.open(`Animal "${animal.name}" has been deleted.`, 'Dismiss', { duration: 3000 });
        });
      }
    });
  }

  export() {
    const data = this.dataSource.getData().map(u => {
      return {
        Name: u.name,
        Age: u.age,
        Country: u.country,
        Species: u.species,
        Subspecies: u.subspecies,
        'Eating habits': u.eatingHabits,
        Type: u.type
      };
    });
    const workSheet: XLSX.WorkSheet = XLSX.utils.json_to_sheet(data);
    const workBook: XLSX.WorkBook = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(workBook, workSheet, 'Animals');
    XLSX.writeFile(workBook, 'animals.xlsx', { bookType: 'xlsx', type: 'buffer' });
  }

  ngOnDestroy(): void { }
}
