import {Component} from '@angular/core';
import {ProductDataService} from "../all-services/product-data.service";
import {Subscription} from "rxjs";
import {IProductRes} from "../../../../models/product.model";
import {ProductService} from "../../../../services/product.service";

@Component({
    selector: 'app-product-details',
    templateUrl: './product-details.component.html',
    styleUrl: './product-details.component.scss'
})
export class ProductDetailsComponent {

    products: IProductRes[] = []
    private productFormSubmittedSubscription!: Subscription;


    constructor(private productService: ProductService,
                private productDataService: ProductDataService) {
    }

    ngOnInit(): void {
        this.fetchProductDetails();
        this.productFormSubmittedSubscription = this.productDataService.productFormSubmitted$.subscribe(() => {
            this.fetchProductDetails();
        })
    }

    ngOnDestroy(): void {
        this.productFormSubmittedSubscription.unsubscribe();
    }

    fetchProductDetails(): void {
        this.productService.getProduct().subscribe(
            (response) => {
                this.products = response.products;
                console.log(this.products);
            },
            (error) => {
                console.log("Error fetching productDetails:", error);
            }
        )
    }


    splitCoverImages(coverImageString: string) {
        if (coverImageString) {
            const url = coverImageString.split('|');
            return url
        }
        return []
    }

    deleteProduct(productId:number)
    {
        this.productService.deleteProduct(productId).subscribe(
            () => {
                this.fetchProductDetails();
            },
        (error: any) => {
                console.log(`Error deleting with Id ${productId}`,error);
    }
        )
    }
}
