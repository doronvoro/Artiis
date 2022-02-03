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
    this.pageResult = new PageResult<Product>();//0, 0, 0,0, []);
  }

  ngOnInit(): void {
    this.Search();
  }

  public ClearSearch() {
    this.textToSearch = "";
    this.Search();

  }
  public onEnter() {
    this.Search();
  }
  public ButtonSearch() {
    this.pageNumber = 1;
    this.Search();
  }

  public Search() {

    var p = this.textToSearch == null ? "" : this.textToSearch ;
    let params = new HttpParams().set('textToSearch', this.textToSearch)
      .set('pageIndex', this.pageNumber)
      .set('pageSize', 5);

    console.log(params);
    console.log(params.toString());


    this.http.get<PageResult<Product>>(this.baseUrl + 'api/products', { params: params })
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
  constructor(public count: number =0,
    public pageIndex: number = 0,
    public pageSize: number = 0,
    public totalPage: number = 0,
    public items: T[] = []) {
   
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

