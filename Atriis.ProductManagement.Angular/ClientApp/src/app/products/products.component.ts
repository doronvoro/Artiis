import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient, HttpParams} from '@angular/common/http';

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


  constructor(public http: HttpClient, @Inject('BASE_URL') public baseUrl: string) {
    this.pageResult = new PageResult<Product>(0, 0, 0, []);
  }

  ngOnInit(): void {
    this.Search();
  }

  public ClearSearch() {
    this.textToSearch = "";
    this.Search();

  } 

  public Search() {

    //this.http.get<Product[]>(this.baseUrl + 'api/test')
    //  .subscribe(result => {
    //    this.products = result;
    //  }, error => console.error(error));



    //let params = new HttpParams().set('productName', this.textToSearch);

    //this.http.get<Product[]>(this.baseUrl + 'api/products', { params: params })
    //  .subscribe(result => {
    //    this.products = result;
    //  }, error => console.error(error));

    var p = this.textToSearch == null ? "" : this.textToSearch ;
    let params1 = new HttpParams().set('textToSearch', this.textToSearch)
      .set('pageIndex', this.pageNumber)
      .set('pageSize', 5);

    this.http.get<PageResult<Product>>(this.baseUrl + 'api/products', { params: params1 })
      .subscribe(result => {

        this.pageResult = result;
        this.products = result.items

        this.pageNumber = result.pageIndex;
        this.Count = result.count;
      
    }, error => console.error(error));


  }


  public onPageChange = (pageNumber: any) => {
    console.log("onPageChange");
    console.log(pageNumber);
    this.pageNumber = pageNumber;
    this.Search();
  }



}


class PageResult<T> {
  constructor(public count: number, public pageIndex: number, public pageSize: number, public items: T[]) {
   
  }
}


//interface PageResult<T>
//{
//  count: number;
//  pageIndex: number;
//  pageSize: number;
//  items: T[];
//}

interface Product {
  sku: number;
  name: number;
  price: number;
  image: string;
}

