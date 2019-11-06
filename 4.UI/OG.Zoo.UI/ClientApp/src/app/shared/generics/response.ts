export interface Response<TEntity> {
    exceptionMessage: string;
    exceptionType: string;
    result: TEntity;
    isSuccess: boolean;
}
