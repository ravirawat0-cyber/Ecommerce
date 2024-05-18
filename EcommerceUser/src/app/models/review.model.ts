export interface IReviewReq{
  productId: number;
  rating: number;
  comments: string;
}


export interface IUserRes {
  id: number;
  name: string;
 // image : IF;
}

export interface IReviewRes {
  id: number;
  productId: number;
  user: IUserRes;
  rating: number;
  comments: string;
}

export interface IReviewResponse {
  statusMessage: string;
  results: number;
  data: IReviewRes[];
}
