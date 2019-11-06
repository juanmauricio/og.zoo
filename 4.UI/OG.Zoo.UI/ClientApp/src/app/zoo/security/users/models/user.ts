import { BaseEntity } from 'src/app/shared/generics/base-entity';

export interface User extends BaseEntity {
    name: string;
    password: string;
    email: string;
}
