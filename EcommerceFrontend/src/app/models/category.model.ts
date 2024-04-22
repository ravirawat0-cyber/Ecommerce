export interface IHttp<T> {
  data: T;
  statusMessage: string;
}

export interface ICategoryForm {
  name: string;
}

export interface ICategoryRes {
  id: number;
  name: string;
}
