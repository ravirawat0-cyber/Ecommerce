export interface IHttp<T>{
  data : T;
  statusMessage: string;
}

export interface IUserReq{
  name : string;
  mobile : string;
  email : string;
  address : string;
  password: string;
  confirmPassword: string;
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
    address : string;
    mobile : string;
  },
  token : {
    jwt : string
  },
  cart : []
}
