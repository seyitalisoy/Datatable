import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ListResponseModel } from '../models/listResponseModel';
import { Link } from '../models/link';
import {  RequestedLinksDto } from '../models/requestedLinksDto';
import { SingleResponseModel } from '../models/singleResponseModel';
import { PageRequestDto } from '../models/pageRequestDto';

@Injectable({
  providedIn: 'root'
})
export class LinkService {

  apiUrl: string = "https://localhost:7170/api/Links/";
  constructor(private httpClient: HttpClient) { }

  getall(): Observable<ListResponseModel<Link>> {
    let newUrl: string = this.apiUrl + "getall";
    return this.httpClient.get<ListResponseModel<Link>>(newUrl);
  }

  getLinksByPagination(pageRequest: PageRequestDto): Observable<SingleResponseModel<RequestedLinksDto>> {
    let newUrl: string =
      this.apiUrl + "getlinksbypagination";
    return this.httpClient
    .get<SingleResponseModel<RequestedLinksDto>>(newUrl,{
      params:{
        pageNumber: pageRequest.pageNumber,
        pageSize : pageRequest.pageSize
      }
    })
  }

}
