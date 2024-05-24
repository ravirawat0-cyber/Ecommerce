export interface IHttp<T> {
  data: T;
  statusMessage: string;
}

export interface IOrderData {
  orderDetails: IOrderDetails[];
}

export interface IOrderDetails {
  totalPrice: number;
  itemDetails: IOrderItem[];
  orderDate: Date;
  transactionId: string;
  receiptURL: string;
}

export interface IOrderItem {
  orderId: number;
  productId: number;
  quantity: number;
  price: number;
  productImage: string;
  productName: string;
}

export interface IOrder {
  id: number;
  userEmail: string;
  transactionId: string;
  userId: number;
  receiptURL: string;
  totalPrice: number;
  orderDate: string;
}
