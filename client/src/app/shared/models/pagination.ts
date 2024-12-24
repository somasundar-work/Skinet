export type Pagination<T> = {
  pageIndex: number;
  pageSize: number;
  count: number;
  data: Array<T>;
};
