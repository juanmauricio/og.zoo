import { BaseEntity } from 'src/app/shared/generics/base-entity';

export interface Animal extends BaseEntity {
    name: string;
    age: number;
    country: string;
    species: string;
    subspecies: string;
    eatingHabits: string;
    type: string;
}
