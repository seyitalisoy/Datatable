import { Component, OnInit } from '@angular/core';
import { LinkService } from '../../services/link.service';
import { Link } from '../../models/link';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-link',
  imports: [CommonModule],
  templateUrl: './link.component.html',
  styleUrl: './link.component.css'
})
export class LinkComponent implements OnInit{

  links : Link[]= [];
  constructor(private linkService: LinkService){}
  
  ngOnInit(): void {
    this.getallLinks();
  }

  getallLinks(){
    this.linkService.getall().subscribe(response=>{
      this.links = response.data;
    })
  }

}
