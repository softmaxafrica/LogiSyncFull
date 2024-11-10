import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class GlobalColumnControlService {
  private columnDialogConfig = {
     responsive: true
  };

  private sourceColumns: any[] = []; // Columns available to add
  private targetColumns: any[] = []; // Columns currently displayed in the table
  first: number = 0;
  rows: number = 10;

  constructor() { }

  // Get the current dialog configuration
  getColumnDialogConfig() {
    return this.columnDialogConfig;
  }

  // Set or update the dialog configuration
  setColumnDialogConfig(config: any) {
    this.columnDialogConfig = { ...this.columnDialogConfig, ...config };
  }

  // Get source columns
  getSourceColumns() {
    return this.sourceColumns;
  }

  // Set source columns
  setSourceColumns(columns: any[]) {
    this.sourceColumns = columns;
  }

  // Get target columns
  getTargetColumns() {
    return this.targetColumns;
  }

  // Set target columns
  setTargetColumns(columns: any[]) {
    this.targetColumns = columns;
  }
  pageChange(event: any): void {
    this.first = event.first;
    this.rows = event.rows;
  }

  
  next(): void {
    this.first += this.rows;
  }

  prev(): void {
    this.first -= this.rows;
  }

  reset(): void {
    this.first = 0;
  }
  isFirstPage(): boolean {
    return this.first === 0;
  }

  
}
