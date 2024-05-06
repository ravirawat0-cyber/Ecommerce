export interface ICartReq{
  productId : number;
}

export interface ICartUpdateReq{
  productId : number;
  quantity : number;
}


export interface ICartItems {
  cart : {
    items: {
      productId: number;
      productName: string;
      productPrice : number;
      quantity : number;
      productImage : string;
    } [],
    totalItems : number;
  },
}
