export interface IHttp<T> {
  data: T;
  statusMessage: string;
}

export interface ISubcategoryForm {
  name: string;
  parentCategoryId: number;
  imageUrl : string;
}

export interface ISubcategoryRes {
  id: number;
  name: string;
  parentCategoryId: number;
  imageUrl : string;
}
