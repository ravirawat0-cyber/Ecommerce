import {Component} from '@angular/core';
import {SubcategoryDataService} from "../all-services/subcategory-data.service";
import {AngularFireStorage, AngularFireUploadTask} from "@angular/fire/compat/storage";
import {finalize, Observable} from "rxjs";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {ICategoryRes} from "../../../../models/category.model";
import {CategoryService} from "../../../../services/category.service";
import {SubcategoryService} from "../../../../services/subcategory.service";
import {ISubcategoryForm} from "../../../../models/subcategory.model";

@Component({
    selector: 'app-subcategoryform',
    templateUrl: './subcategoryform.component.html',
    styleUrl: './subcategoryform.component.scss',
})
export class SubcategoryformComponent {
    categories: ICategoryRes[] = [];
    formGroup: FormGroup;
    errorMessage: string = '';

    constructor(
        private formBuilder: FormBuilder,
        private categoryService: CategoryService,
        private subcategoryService: SubcategoryService,
        private subCategoryDatService: SubcategoryDataService,
        private fireStorage: AngularFireStorage,
    ) {
        this.formGroup = this.formBuilder.group({
            parentCategoryId: ['', Validators.required],
            subcategoryName: ['', Validators.required],
            imageUrl: ['', Validators.required],
        })

    }

    ngOnInit(): void {
        this.fetchCategories();
    }

    async UploadSingleImages(event: any) {
        const file = event.target.files[0];

        if (file) {
            const path = `subCategorys/${file.name}`;
            const uploadTask: AngularFireUploadTask = this.fireStorage.upload(path, file);

            uploadTask
                .snapshotChanges()
                .pipe(
                    finalize(() => {
                        const downloadURL$: Observable<string> = this.fireStorage
                            .ref(path)
                            .getDownloadURL();
                        downloadURL$.subscribe(
                            (downloadURL: string) => {
                                this.formGroup.patchValue({imageUrl: downloadURL});
                            },
                            (error) => {
                                this.errorMessage = error;
                            }
                        );
                    })
                ).subscribe();
        }
    }


    fetchCategories() {
        this.categoryService.getCategory().subscribe(
            (response) => {
                this.categories = response.data;
            },
            (error) => {
                this.errorMessage = "Error featching category."
            }
        );
    }

    handleSubmit() {
        if (this.formGroup.valid) {
            const subcategory: ISubcategoryForm = {
                name: this.formGroup.value.subcategoryName,
                parentCategoryId: this.formGroup.value.parentCategoryId,
                imageUrl: this.formGroup.value.imageUrl
            }

            this.subcategoryService.addSubcategory(subcategory).subscribe((res) => {
                    this.errorMessage = '';
                    this.handleFormReset();
                    this.subCategoryDatService.notifySubcategorySubmitted();

                },
                (error) => {
                    this.errorMessage = error.error;
                    console.log(error.error)
                    this.handleFormReset()
                }
            );
        }
    }

    handleFormReset() {
        this.formGroup.reset({
            parentCategoryId: '',
            subcategoryName: '',
            imageUrl: ''
        })
    }

    onSelectCategory(event: any) {
        this.formGroup.patchValue({parentCategoryId: event.target.value});
    }
}
