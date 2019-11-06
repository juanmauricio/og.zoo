import { BaseEntity } from 'src/app/shared/generics/base-entity';
import { Animal } from '../../animals/models/animal';

export interface Infirmary extends BaseEntity {
    idAnimal: string;
    admissionDate: Date;
    diagnosis: string;
    animal: Animal;
}
