import {Component} from '@angular/core';

import {
    AngularFireStorage,
    AngularFireUploadTask,
} from '@angular/fire/compat/storage';
import {Observable, finalize} from 'rxjs';

import {ProductDataService} from "../all-services/product-data.service";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {ISubcategoryRes} from "../../../../models/subcategory.model";
import {SubcategoryService} from "../../../../services/subcategory.service";
import {ProductService} from "../../../../services/product.service";
import {IProductForm} from "../../../../models/product.model";

@Component({
    selector: 'app-productform',
    templateUrl: './productform.component.html',
    styleUrl: './productform.component.scss',
})
export class ProductformComponent {

    subCategories: ISubcategoryRes[] = [];
    formGroup: FormGroup;
    downloadURLs: string[] = [];
    errorMessage: string = '';

    constructor(
        private subCategoryServie: SubcategoryService,
        private fireStorage: AngularFireStorage,
        private productService: ProductService,
        private productDataService: ProductDataService,
        private formBuilder: FormBuilder
    ) {

        this.formGroup = this.formBuilder.group(
            {
                productName: ['', Validators.required],
                productDescription: ['', Validators.required],
                price: ['', Validators.required],
                brand: ['', Validators.required],
                keyFeature: ['', Validators.required],
                imageUrl: ['', Validators.required],
                selectedSubCategoryId: ['', Validators.required],
                coverImageUrl: ['', Validators.required]
            }
        )
    }

    ngOnInit(): void {
        this.fetchSubCategories();
    }

    async UploadSingleImages(event: any) {
        const file = event.target.files[0];

        if (file) {
            const path = `products/${file.name}`;
            const uploadTask: AngularFireUploadTask = this.fireStorage.upload(
                path,
                file
            );

            uploadTask
                .snapshotChanges()
                .pipe(
                    finalize(() => {
                        const downloadURL$: Observable<string> = this.fireStorage
                            .ref(path)
                            .getDownloadURL();
                        downloadURL$.subscribe(
                            (downloadURL: string) => {
                                this.formGroup.patchValue({coverImageUrl: downloadURL})  /// Set the download UR
                            },
                            (error) => {
                                this.errorMessage = error;
                            }
                        );
                    })
                )
                .subscribe();
        }
    }

    async UploadMultiImages(event: any) {
        const files = event.target.files;

        if (files && files.length > 0) {
            for (let i = 0; i < files.length; i++) {
                const path = `products/${files[i].name}`;
                const uploadTask: AngularFireUploadTask = this.fireStorage.upload(
                    path,
                    files[i]
                );

                uploadTask
                    .snapshotChanges()
                    .pipe(
                        finalize(() => {
                            const downloadURL$: Observable<string> = this.fireStorage
                                .ref(path)
                                .getDownloadURL();

                            downloadURL$.subscribe(
                                (downloadURL: string) => {
                                    this.downloadURLs.push(downloadURL);
                                    this.updateImageUrl();
                                },
                                (error) => {
                                    this.errorMessage = error;
                                }
                            );
                        })
                    )
                    .subscribe();
            }
        }
    }

    updateImageUrl() {
        this.formGroup.patchValue({imageUrl: this.downloadURLs.join('|')});
    }

    fetchSubCategories() {
        this.subCategoryServie.getSubcategory().subscribe(
            (response) => {
                this.subCategories = response.data;
            },
            (error) => {
                this.errorMessage = "Unable to fetch subCategory"
            }
        );
    }

    onSelectSubCategory(event: any) {
        this.formGroup.patchValue({selectedSubCategoryId: event.target.value});
    }


    handleSubmit() {
        try {
            if (!this.isFromFilled) {
                this.errorMessage = 'Please fill in all required fields and upload images.'
            }

            const newProduct: IProductForm = {
                name: this.formGroup.value.productName,
                subCategoryId: this.formGroup.value.selectedSubCategoryId,
                description: this.formGroup.value.productDescription,
                price: this.formGroup.value.price,
                companyName: this.formGroup.value.brand,
                keyFeature: this.formGroup.value.keyFeature,
                coverImage: this.formGroup.value.coverImageUrl,
                imageUrls: this.formGroup.value.imageUrl,
            };

            this.productService.addProduct(newProduct).subscribe(
                (res) => {
                    this.productDataService.notifyProductFormSubmitted(); // Notify submission
                    this.resetForm();
                },
                (error) => {
                    this.errorMessage = 'Error submitting product';
                }
            );
        } catch (error) {
            this.errorMessage = 'Error in form submission';
        }
    }

    get isFromFilled(): boolean {
        console.log(this.formGroup.value.keyFeature);
        return (
            this.formGroup.valid && this.downloadURLs.length > 0
        );
    }

    resetForm() {
        this.formGroup.reset({
            productName: '',
            productDescription: '',
            price: "",
            brand: '',
            keyFeature: '',
            coverImageUrl: '',
            imageUrl: '',
        });
        this.subCategories = [];
        this.downloadURLs = [];
    }
}
