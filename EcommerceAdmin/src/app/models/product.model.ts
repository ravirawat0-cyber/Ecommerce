

export interface IHttp<T> {
  products: T;
  statusMessage: string;
}

export interface IProductForm {
  name: string;
  description: string;
  price: number;
  companyName: string;
  subCategoryId: number;
  keyFeature: string;
  coverImage: string;
  imageUrls: string;
}

export interface IProductRes {
  productId : number;
  productName: string;
  categories: {
    parentcategory : {
      categoryId : number;
      categoryName: string;
    }
    subcategories : {
      subCategoryId :number;
      subCategoryName:string;
    }
  }
  price: number;
  description: string;
  companyName: string;
  keyfeature: string;
  coverImage: string;
  imageUrls: string;
}
