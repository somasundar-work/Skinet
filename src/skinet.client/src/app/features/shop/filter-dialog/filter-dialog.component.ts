import { Component, inject } from '@angular/core';
import { ShopService } from '../../../core/services/shop.service';
import { MatDividerModule } from '@angular/material/divider';
import { MatListOption, MatSelectionList } from '@angular/material/list';
import { MatButtonModule } from '@angular/material/button';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FormsModule } from '@angular/forms';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-filter-dialog',
  imports: [
    MatDividerModule,
    MatSelectionList,
    MatListOption,
    MatButtonModule,
    FormsModule,
    MatIconModule,
  ],
  templateUrl: './filter-dialog.component.html',
  styleUrl: './filter-dialog.component.scss',
})
export class FilterDialogComponent {
  shopService = inject(ShopService);
  private dialogRef = inject(MatDialogRef<FilterDialogComponent>);
  data = inject(MAT_DIALOG_DATA);

  selectedCategories: string[] = this.data.selectedCategories;
  selectedBrands: string[] = this.data.selectedBrands;

  applyFilters() {
    this.dialogRef.close({
      selectedCategories: this.selectedCategories,
      selectedBrands: this.selectedBrands,
    });
  }

  clearFilters() {
    this.selectedCategories = [];
    this.selectedBrands = [];
    this.applyFilters();
  }
}
