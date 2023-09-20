export interface Page<T> {
  pageIndex: number;
  limit: number;
  total: number;
  data: T[];
}
