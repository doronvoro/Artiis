import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient, HttpParams} from '@angular/common/http';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {

  public products: Product[] = [];

  textToSearch: string = '';


  constructor(public http: HttpClient, @Inject('BASE_URL')  public baseUrl: string) {
  }

  ngOnInit(): void {
    this.Search();
  }

  public ClearSearch() {
    this.textToSearch = "";
    this.Search();

  }

  public Search() {

    let params = new HttpParams().set('productName', this.textToSearch);

    this. http.get<Product[]>(this.baseUrl + 'api/products', { params: params })
      .subscribe(result => {
        this.products = result;
      }, error => console.error(error));

  }

}

interface Product {
  sku: number;
  name: number;
  price: number;
  image: string;
}

