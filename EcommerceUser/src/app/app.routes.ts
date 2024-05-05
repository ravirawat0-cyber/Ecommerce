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

export const routes: Routes = [
  {path: 'home', component: HomeContainerComponent},
  {path: 'category/:name/:id' , component: SubcategoriesByParentContainerComponent},
  {path: 'subcategory/:name/:id', component:SubcategoryProductsComponent },
  {path: 'product/:name/:id' , component: ProductDetailsComponent},
  {path: '', redirectTo : '/home', pathMatch: 'full'},
  {path: 'register' , component: RegisterComponent},
  {path: 'login', component: LoginComponent},
  {path: 'cart', component: CartComponent},
  {path: '**', component: PageNotFoundComponent}
];
