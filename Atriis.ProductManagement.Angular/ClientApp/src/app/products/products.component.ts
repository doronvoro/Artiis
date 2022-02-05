import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
 
import { MatDialogModule, MatDialog, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ProductDetailDialogComponent } from '../product-detail-dialog/product-detail-dialog.component';

//import { MatPaginator } from '@angular/material/paginator';
//import { MatSort, SortDirection } from '@angular/material/sort';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {

  public products: Product[] = [];
  public pageResult: PageResult<Product>;
  public pageNumber: number = 1;
  public Count: number =0;
  textToSearch: string = '';
  displayedColumns: string[] = ['sku', 'price', 'image', 'name'   ];

  constructor(public http: HttpClient,
              @Inject('BASE_URL') public baseUrl: string,
    public dialog: MatDialog) {
    this.pageResult = new PageResult<Product>(); 
  }

  ngOnInit(): void {
    this.Search();
  }

  public ClearSearch() {
    this.textToSearch = "";
    this.ButtonSearch();
  }
  public onEnter() {
    this.ButtonSearch();
  }
  public ButtonSearch() {
    this.pageNumber = 1;
    this.Search();
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
                                 .set('pageSize', 5);
     
    //console.log(params.toString());

    this.http.get<PageResult<Product>>(this.baseUrl + 'api/products', { params: params })
      .subscribe(result => {

        this.pageResult = result;
        this.products = result.items

        this.pageNumber = result.pageIndex;
        this.Count = result.count;
           
    }, error => console.error(error));

  }

  public onPageChange = (pageNumber: any) => {
    this.pageNumber = pageNumber;
    this.Search();
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

