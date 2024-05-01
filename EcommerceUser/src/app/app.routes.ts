import { Routes } from '@angular/router';
import {HomeContainerComponent} from "./home/home-container/home-container.component";
import {PageNotFoundComponent} from "./global/page-not-found/page-not-found.component";
import {
  SubcategoriesByParentContainerComponent
} from "./specificCategory/subcategories-by-parent-container/subcategories-by-parent-container.component";
import {SubcategoryProductsComponent} from "./subcategory-products/subcategory-products.component";
import {ProductDetailsComponent} from "./product-details/product-details.component";

export const routes: Routes = [
  {path: 'home', component: HomeContainerComponent},
  {path: 'category/:name/:id' , component: SubcategoriesByParentContainerComponent},
  {path: 'subcategory/:name/:id', component:SubcategoryProductsComponent },
  {path: 'product/:name/:id' , component: ProductDetailsComponent},
  {path: '', redirectTo : '/home', pathMatch: 'full'},
  {path: '**', component: PageNotFoundComponent}
];
