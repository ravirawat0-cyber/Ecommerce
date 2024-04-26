import { NgModule } from '@angular/core';
import {
  BrowserModule,
  provideClientHydration,
} from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { CommonModule } from '@angular/common';
import { AppComponent } from './app.component';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { NavbarComponent } from './components/AdminDashboard/navbar/navbar.component';
import { NgIconsModule } from '@ng-icons/core';
import { heroUsers, heroShoppingCart } from '@ng-icons/heroicons/outline';
import { SelectServicesComponent } from './components/AdminDashboard/select-services/select-services.component';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AngularFireModule } from '@angular/fire/compat';
import { AngularFireStorageModule } from '@angular/fire/compat/storage';
import { DataToolMainComponent } from './components/AdminDashboard/data-tool/data-tool-main/data-tool-main.component';
import { DashboardContainerComponent } from './components/AdminDashboard/data-tool/dashboard-container/dashboard-container.component';
import { SelectServicesService } from './components/AdminDashboard/select-services/select-services.service';
import {ProductformComponent} from "./components/AdminDashboard/data-services/productform/productform.component";
import {
    SubcategoryformComponent
} from "./components/AdminDashboard/data-services/subcategoryform/subcategoryform.component";
import {CategoryformComponent} from "./components/AdminDashboard/data-services/categoryform/categoryform.component";
import {
    DataDashboardComponent
} from "./components/AdminDashboard/data-services/data-dashboard/data-dashboard.component";
import {
    ProductDetailsComponent
} from "./components/AdminDashboard/data-services/product-details/product-details.component";
import {
    CategoryDetailsComponent
} from "./components/AdminDashboard/data-services/category-details/category-details.component";
import {
    SubcategoryDetailsComponent
} from "./components/AdminDashboard/data-services/subcategory-details/subcategory-details.component";


const firebaseConfig = {
  apiKey: 'AIzaSyDHx50TkDkORTXxmMcn-EIL0P4sFCS5oAQ',
  authDomain: 'ecommerce-55ed9.firebaseapp.com',
  projectId: 'ecommerce-55ed9',
  storageBucket: 'ecommerce-55ed9.appspot.com',
  messagingSenderId: '112985743744',
  appId: '1:112985743744:web:328cc750e2a03c54d1e165',
  measurementId: 'G-Q52WW17P5D',
};



@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    SelectServicesComponent,
    ProductformComponent,
    SubcategoryformComponent,
    CategoryformComponent,
    DataDashboardComponent,
    ProductDetailsComponent,
    DataToolMainComponent,
    DashboardContainerComponent,
    CategoryDetailsComponent,
    SubcategoryDetailsComponent,
  ],
    imports: [
        BrowserModule,
        AppRoutingModule,
        NgIconsModule.withIcons({heroUsers, heroShoppingCart}),
        FormsModule,
        CommonModule,
        HttpClientModule,
        AngularFireModule.initializeApp(firebaseConfig),
        AngularFireStorageModule,
        ReactiveFormsModule,
    ],
  providers: [
    provideClientHydration(),
    provideAnimationsAsync(),
    SelectServicesService,
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
