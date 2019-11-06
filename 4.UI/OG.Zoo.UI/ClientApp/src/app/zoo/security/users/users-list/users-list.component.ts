import { Component, OnInit, ViewChild, OnDestroy } from '@angular/core';
import { MatPaginator, MatSort, MatTable, MatDialog, MatSnackBar } from '@angular/material';
import { UserService } from '../services/user.service';
import { User } from '../models/user';
import { ConfirmComponent } from 'src/app/shared/dialogs/confirm/confirm.component';
import { untilComponentDestroyed } from '@w11k/ngx-componentdestroyed';
import * as XLSX from 'xlsx';
import { ServerSideListDataSource } from 'src/app/shared/generics/server-side-list-datasource';

@Component({
  selector: 'app-users-list',
  templateUrl: './users-list.component.html',
  styleUrls: ['./users-list.component.scss']
})
export class UsersListComponent implements OnInit, OnDestroy {
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild(MatTable) table: MatTable<any>;

  dataSource: ServerSideListDataSource<User>;

  /** Columns displayed in the table. Columns IDs can be added, removed, or reordered. */
  displayedColumns = ['name', 'actions'];

  constructor(
    private dialog: MatDialog,
    private snackBar: MatSnackBar,
    private userService: UserService) {
  }

  ngOnInit() {
    this.dataSource = new ServerSideListDataSource(this.paginator, this.sort);
    this.dataSource.setDataSource((params: {[x: string]: any}) => this.userService.getPaginated(params));
  }

  refresh() {
    this.dataSource.updateData();
  }

  delete(user: User) {
    const dialogRef = this.dialog.open(ConfirmComponent, { width: '250px', data: { content: 'Are you sure you want to delete it?' } });
    dialogRef.afterClosed().pipe(untilComponentDestroyed(this)).subscribe(result => {
      if (result) {
        this.userService.delete(user.id).pipe(untilComponentDestroyed(this)).subscribe(() => {
          this.refresh();
          this.snackBar.open(`User "${user.name}" has been deleted.`, 'Dismiss', { duration: 3000 });
        });
      }
    });
  }

  export() {
    const data = this.dataSource.getData().map(u => {
      return {
        Name: u.name
      };
    });
    const workSheet: XLSX.WorkSheet = XLSX.utils.json_to_sheet(data);
    const workBook: XLSX.WorkBook = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(workBook, workSheet, 'Users');
    XLSX.writeFile(workBook, 'users.xlsx', { bookType: 'xlsx', type: 'buffer' });
  }

  ngOnDestroy(): void { }
}
