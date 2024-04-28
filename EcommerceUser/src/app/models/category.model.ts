export interface IHttp<T>
{
  data: T;
  statusMessage : string;
}

export interface ICategoryRes{
  id : number;
  name : string;
}

export interface ICategoryDataRes
{
  id: number;
  category: string;
  subcategories : {
    id : number;
    name : string;
  }
}
