export interface IHttp<T>
{
  data : T;
  statusMessage: string;
}

export interface IOrderRes{
  Id : number;
  userEmail : string;
  transactionId :string;
  receiptURL : string;
  totalPrice : string;
  orderDate : string;
}
