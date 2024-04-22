import { NgModule } from '@angular/core';
import {
  BrowserModule,
  provideClientHydration,
} from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { CommonModule } from '@angular/common';
import { AppComponent } from './app.component';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { NavbarComponent } from './components/navbar/navbar.component';
import { ProductFormComponent } from './components/products/product-form/product-form.component';
import { NgIconsModule } from '@ng-icons/core';
import { heroUsers, heroShoppingCart } from '@ng-icons/heroicons/outline';
import { SelectServicesComponent } from './components/select-services/select-services.component';
import { FormsModule } from '@angular/forms';
import { ProductformComponent } from './components/data-services/productform/productform.component';
import { SubcategoryformComponent } from './components/data-services/subcategoryform/subcategoryform.component';
import { CategoryformComponent } from './components/data-services/categoryform/categoryform.component';
import { DataDashboardComponent } from './components/data-services/data-dashboard/data-dashboard.component';
import { HttpClientModule } from '@angular/common/http';
@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    ProductFormComponent,
    SelectServicesComponent,
    ProductformComponent,
    SubcategoryformComponent,
    CategoryformComponent,
    DataDashboardComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgIconsModule.withIcons({ heroUsers, heroShoppingCart }),
    FormsModule,
    CommonModule,
    HttpClientModule,
  ],
  providers: [provideClientHydration(), provideAnimationsAsync()],
  bootstrap: [AppComponent],
})
export class AppModule {}
