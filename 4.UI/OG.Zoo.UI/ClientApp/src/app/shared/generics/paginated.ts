export interface Paginated<TEntity> {
    page: number;
    itemsPerPage: number;
    totalItems: number;
    items: TEntity[];
}
