import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { MatDialog} from '@angular/material/dialog';
import { ProductDetailDialogComponent } from '../product-detail-dialog/product-detail-dialog.component';
import { DOCUMENT } from '@angular/common';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {

  public products: Product[] = [];
  //public pageResult: PageResult<Product>;
  public pageNumber: number = 1;
  public pageSize: number = 5;
  public count: number = 0;
  public textToSearch: string = '';
  public defaultSortCoulmn: string = 'sku.asc';
  public sortCoulmn: string = this.defaultSortCoulmn;

  constructor(public http: HttpClient,
              public dialog: MatDialog,
              @Inject('BASE_URL') public baseUrl: string,
              @Inject(DOCUMENT) private document: Document  ) {
   // this.pageResult = new PageResult<Product>(); 
  }

  ngOnInit(): void {
    this.Search();
  }

  public ClearSearch() {
    this.textToSearch = "";
    this.ResetSearch();
  }
  
  public ResetSearch() {
    this.pageNumber = 1;
    this.sortCoulmn = this.defaultSortCoulmn;
    this.setColumnSortIcon();
    this.Search();
  }

  public onPageChange = (pageNumber: any) => {
    this.pageNumber = pageNumber;
    this.Search();
  }

  public onSearchClick = () => {
    this.ResetSearch();
  }

  public onPageSizeChange = (pageSize: any) => {
    this.pageSize = pageSize;
    this.ResetSearch();
  }

  public Sort(columnName: string) {

    var arr = this.sortCoulmn.split('.');

    if (arr[0] == columnName) {
      var order = (arr[1] == 'asc') ? "dsc" : "asc";
      this.sortCoulmn = `${columnName}.${order}`;
    }
    else {
      this.sortCoulmn = `${columnName}.asc`;
    }

    this.setColumnSortIcon();
    this.pageNumber = 1;
    this.Search();
  }

  setColumnSortIcon(): void {
    var up = "bi bi-arrow-up";
    var down = "bi bi-arrow-down"
    let allowCoulmnsSort: string[] = ['sku', 'name'];

    for (let columnName of allowCoulmnsSort) {

      var arrow = "";
      var arr = this.sortCoulmn.split('.');
      if (arr[0] == columnName) {
        arrow = (arr[1] == 'asc') ? up : down;
      }
      var id = "sort_" + columnName;
      var element = document.getElementById(id) as HTMLElement;;
      element.className = arrow;// arrow;
    }
  }

  public openDialog(sku : any)
  {
    this.dialog.open(ProductDetailDialogComponent, {
      data: {
        sku: sku,
      },
    });

}

  public Search() {

    let params = new HttpParams().set('textToSearch', this.textToSearch)
      .set('pageIndex', this.pageNumber)
      .set('pageSize', this.pageSize) 
      .set('sortCoulmn',this.sortCoulmn )

    //console.log(params.toString());

    this.http.get<PageResult<Product>>(this.baseUrl + 'api/products', { params: params })
      .subscribe(result => {

        //this.pageResult = result;
        this.products = result.items

        this.pageNumber = result.pageIndex;
        this.count = result.count;
           
    }, error => console.error(error));
  }
}

class PageResult<T> {
  constructor(public count: number =0,
    public pageIndex: number = 0,
    public pageSize: number = 0,
    public totalPage: number = 0,
    public items: T[] = []) {
   
  }
}
 
interface Product {
  sku: number;
  name: number;
  price: number;
  image: string;
}
