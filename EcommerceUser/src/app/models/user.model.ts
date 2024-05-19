export interface IHttp<T>{
  data : T;
  statusMessage: string;
}

export interface IUserReq{
  name : string;
  mobile : string;
  email : string;
  image : string;
  address : string;
  password: string;
  confirmPassword: string;
}

export interface IUserUpdateReq{
  name : string;
  mobile : string;
  image : string;
  address : string;
}

export interface IUserLoginReq{
  email : string;
  password : string;
}

export interface IUserRes{
  user : {
    id : number;
    name : string;
    email : string;
    image : string;
    address : string;
    mobile : string;
    joinedDate : string;
  },
  token : {
    jwt : string
  },
  cart : {
    items: [ {
      productId: number;
      productName: string;
      productPrice : number;
      quantity : number;
      productImage : string;
    } ],
    totalItems : number;
    totalPrice : number;
  },
  wishlist: {
    items: [
      {
        productId: number;
        productName: string;
        productPrice : number;
        productImage : string;
      }
    ],
    totalItems : number;
  },
  statusMessage : string;
}
