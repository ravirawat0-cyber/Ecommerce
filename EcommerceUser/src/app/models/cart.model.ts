export interface ICartReq{
  productId : number;
}

export interface ICartUpdateReq{
  productId : number;
  quantity : number;
}


export interface IPurchaseRes{
  url: string;
}
