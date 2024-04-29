export interface IHttp<T>{
  data: T;
  statusMessage: string;
}

export interface ISubcategoryRes{
  id : number;
  name : string;
  imageUrl : string;
}
