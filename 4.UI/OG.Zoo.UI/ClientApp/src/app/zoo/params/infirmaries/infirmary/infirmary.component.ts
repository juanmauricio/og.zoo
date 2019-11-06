import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormBuilder, Validators, AbstractControl, ValidatorFn } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { MatSnackBar } from '@angular/material';
import { untilComponentDestroyed } from '@w11k/ngx-componentdestroyed';
import { InfirmaryService } from '../services/infirmary.service';
import { Infirmary } from '../models/infirmary';
import { AnimalService } from '../../animals/services/animal.service';
import { Animal } from '../../animals/models/animal';
import { Observable } from 'rxjs';
import { startWith, map, finalize } from 'rxjs/operators';
import { isUndefined, isNullOrUndefined } from 'util';
import { BaseEntity } from 'src/app/shared/generics/base-entity';
import { LoadingService } from 'src/app/shared/loading/loading.service';

@Component({
  selector: 'app-infirmary',
  templateUrl: './infirmary.component.html',
  styleUrls: ['./infirmary.component.css']
})
export class InfirmaryComponent implements OnInit, OnDestroy {
  editMode: boolean;
  infirmaryForm = this.fb.group({
    id: [null],
    animal: [null, Validators.compose([Validators.required])],
    admissionDate: [null],
    idAnimal: [null],
    diagnosis: [null, Validators.required]
  });

  animals: Animal[];
  filteredAnimals: Observable<Animal[]>;

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private snackBar: MatSnackBar,
    private infirmaryService: InfirmaryService,
    private animalService: AnimalService,
    private loadingService: LoadingService) { }

  ngOnInit() {
    this.getAnimals();
    this.route.params.pipe(untilComponentDestroyed(this)).subscribe((data) => {
      this.editMode = typeof (data.id) !== 'undefined';
      if (this.editMode) {
        this.get(data.id);
      }
    });
  }

  get(id: string) {
    this.infirmaryService.getWithRelations(id).pipe(untilComponentDestroyed(this)).subscribe((infirmary) => {
      this.infirmaryForm.setValue(infirmary);
    });
  }

  getAnimals() {
    return this.animalService.getAll().pipe(untilComponentDestroyed(this)).subscribe((animals) => {
      this.animals = animals;
      this.infirmaryForm.get('animal').setValidators([Validators.required, this.listValidator<Animal>(animals)]);
      this.filteredAnimals = this.infirmaryForm.get('animal').valueChanges
        .pipe(
          startWith<string | Animal>(''),
          map((value) => typeof value === 'string' ? value : value.name),
          map((name) => {
            const values = name ? this.animals.filter(option =>
              option.name.toLocaleLowerCase().includes(name.toLocaleLowerCase())) : this.animals.slice();
            return values;
          })
        );
    });
  }

  displayAnimal(entity: Animal) {
    return entity ? entity.name : undefined;
  }

  onSubmit() {
    if (this.infirmaryForm.valid) {
      this.loadingService.show();
      const finalizeFunction = finalize(() => this.loadingService.hide());
      const infirmary: Infirmary = this.infirmaryForm.value;
      infirmary.idAnimal = `Animal/${infirmary.animal.id}`;
      if (this.editMode) {
        this.infirmaryService.update(infirmary).pipe(untilComponentDestroyed(this), finalizeFunction).subscribe(() => {
          this.snackBar.open(`Infirmary "${infirmary.admissionDate}" has been updated.`, 'Dismiss', { duration: 3000 });
          this.goBack();
        });
      } else {
        infirmary.admissionDate = new Date();
        this.infirmaryService.create(infirmary).pipe(untilComponentDestroyed(this), finalizeFunction).subscribe(() => {
          this.snackBar.open(`Infirmary "${infirmary.admissionDate}" has been created.`, 'Dismiss', { duration: 3000 });
          this.goBack();
        });
      }
    }
  }

  goBack() {
    this.router.navigate(['params/infirmaries']);
  }

  listValidator<T extends BaseEntity>(list: T[]): ValidatorFn {
    return (control: AbstractControl): { [key: string]: boolean } | null => {
      if (!isNullOrUndefined(control.value) && isUndefined(list.find(l => l.id === control.value.id))) {
        return { invalidList: true };
      }
      return null;
    };
  }

  clearInput(inputName: string) {
    this.infirmaryForm.controls[inputName].setValue('');
    setTimeout(() => {
      const doc = document.activeElement as HTMLInputElement;
      doc.blur();
    });
  }

  ngOnDestroy(): void { }
}
