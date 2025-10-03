import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ListResponseModel } from '../models/listResponseModel';
import { Link } from '../models/link';

@Injectable({
  providedIn: 'root'
})
export class LinkService {
  
  apiUrl :string = "https://localhost:7170/api/Links/";
  constructor(private httpClient : HttpClient){}

  getall():Observable<ListResponseModel<Link>>{
    let newUrl : string = this.apiUrl+"getall";
    return this.httpClient.get<ListResponseModel<Link>>(newUrl);
  }

}
