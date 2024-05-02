export interface IHttp<T>
{
  products : T;
  statusMessage : string;
}

export interface IProductRes
{
  id : number;
  name : string;
  coverImage : string;
  keyFeature: string;
  price : string;
}

export interface IProductProfileRes{
  id : number;
  name : string;
  coverImage : string;
  keyFeature : string;
  price : string;
  description : string;
  imageUrls : string;
}
