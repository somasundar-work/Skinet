import { Component, inject, OnInit } from '@angular/core';
import { ShopService } from '../../core/services/shop.service';
import { Product } from '../../shared/models/product';
import { ProductItemComponent } from './product-item/product-item.component';
import { MatDialog } from '@angular/material/dialog';
import { FilterDialogComponent } from './filter-dialog/filter-dialog.component';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import {
  MatListOption,
  MatSelectionList,
  MatSelectionListChange,
} from '@angular/material/list';
import { MatMenuModule, MatMenuTrigger } from '@angular/material/menu';
import { ShopParams } from '../../shared/models/shopParams';
import { MatPaginatorModule, PageEvent } from '@angular/material/paginator';
import { Pagination } from '../../shared/models/pagination';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrl: './shop.component.scss',
  imports: [
    ProductItemComponent,
    MatButtonModule,
    MatIconModule,
    MatMenuModule,
    MatSelectionList,
    MatListOption,
    MatMenuTrigger,
    MatPaginatorModule,
    FormsModule,
  ],
})
export class ShopComponent implements OnInit {
  private shopService = inject(ShopService);
  private dialogService = inject(MatDialog);

  products?: Pagination<Product>;
  sortOptions = [
    { value: 'name', display: 'Alphabetical' },
    { value: 'priceAsc', display: 'Price: Low-High' },
    { value: 'priceDesc', display: 'Price: High-Low' },
  ];
  shopParams: ShopParams = new ShopParams();
  pageSizeOptions = [6, 12, 24, 48];

  ngOnInit(): void {
    this.initializeShop();
  }

  initializeShop() {
    this.shopService.getCategrories();
    this.shopService.getBrands();
    this.getProducts();
  }

  getProducts() {
    this.shopService.getProducts(this.shopParams).subscribe({
      next: (response) => (this.products = response),
      error: (error) => console.log(error),
    });
  }

  onSortChange(event: MatSelectionListChange) {
    const selectedOption = event.options[0];
    if (selectedOption) {
      this.shopParams.sort = selectedOption.value;
      this.shopParams.pageNumber = 1;
      this.getProducts();
    }
  }

  openFilterDialog() {
    const dialogRef = this.dialogService.open(FilterDialogComponent, {
      disableClose: true,
      panelClass: ['sundar', 'lg:w-4/12', 'w-full'],
      data: {
        selectedBrands: this.shopParams.brands,
        selectedCategories: this.shopParams.categories,
      },
    });

    dialogRef.afterClosed().subscribe({
      next: (result) => {
        this.shopParams.categories = result.selectedCategories;
        this.shopParams.brands = result.selectedBrands;
        this.shopParams.pageNumber = 1;
        this.getProducts();
      },
    });
  }

  handlePageEvent(event: PageEvent) {
    this.shopParams.pageNumber = event.pageIndex + 1;
    this.shopParams.pageSize = event.pageSize;
    this.getProducts();
  }

  onSearchChange() {
    this.shopParams.pageNumber = 1;
    this.getProducts();
  }
}
