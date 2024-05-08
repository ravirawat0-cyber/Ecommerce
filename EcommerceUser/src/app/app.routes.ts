import { Routes } from '@angular/router';
import {HomeContainerComponent} from "./home/home-container/home-container.component";
import {PageNotFoundComponent} from "./global/page-not-found/page-not-found.component";
import {
  SubcategoriesByParentContainerComponent
} from "./specificCategory/subcategories-by-parent-container/subcategories-by-parent-container.component";
import {SubcategoryProductsComponent} from "./subcategory-products/subcategory-products.component";
import {ProductDetailsComponent} from "./product-details/product-details.component";
import {RegisterComponent} from "./Auth/register/register.component";
import {LoginComponent} from "./Auth/login/login.component";
import {CartComponent} from "./cart/cart.component";
import {WishlistComponent} from "./wishlist/wishlist.component";
import {AccountComponent} from "./Auth/account/account.component";

export const routes: Routes = [
  {path: 'home', component: HomeContainerComponent},
  {path: 'category/:name/:id' , component: SubcategoriesByParentContainerComponent},
  {path: 'subcategory/:name/:id', component:SubcategoryProductsComponent },
  {path: 'product/:name/:id' , component: ProductDetailsComponent},
  {path: 'register' , component: RegisterComponent},
  {path: 'login', component: LoginComponent},
  {path: 'cart', component: CartComponent},
  {path: 'wishlist', component: WishlistComponent},
  {path : 'account', component: AccountComponent},
  {path: '', redirectTo : '/home', pathMatch: 'full'},
  {path: '**', component: PageNotFoundComponent}
];
