import { Component, OnInit, Inject} from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import {MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-product-detail-dialog',
  templateUrl: './product-detail-dialog.component.html',
  styleUrls: ['./product-detail-dialog.component.css']
})
export class ProductDetailDialogComponent implements OnInit {

  constructor(public http: HttpClient,
             @Inject('BASE_URL') public baseUrl: string,
    @Inject(MAT_DIALOG_DATA) public data: DialogData,
    private dialogRef: MatDialogRef<ProductDetailDialogComponent>   ) { }

  ngOnInit(): void {
    this.Load();
  }
  public result: any;

  public CloseDialog() {
    this.dialogRef.close();
  }

  public Load() {

    let params = new HttpParams().set('sku', this.data.sku);
    //console.log(params.toString());
    this.http.get<any>(this.baseUrl + 'api/productDetails', { params: params })
      .subscribe(result => {
        this.result = result;
      }, error => console.error(error));
  }
}
export interface DialogData {
  sku: number;
}
