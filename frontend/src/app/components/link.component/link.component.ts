import { Component, OnInit } from '@angular/core';
import { LinkService } from '../../services/link.service';
import { Link } from '../../models/link';
import { CommonModule } from '@angular/common';
import { LucideAngularModule, ArrowDown, ArrowUp, ChevronLeft, ChevronRight,Plus,X } from 'lucide-angular';
import { PageRequestDto } from '../../models/pageRequestDto';
import { FormsModule } from '@angular/forms';
import { FilterComponent } from '../filter.component/filter.component';
import { LinkAddComponent } from '../link-add.component/link-add.component';

@Component({
  selector: 'app-link',
  imports: [
    CommonModule,
    LucideAngularModule,
    FormsModule,
    FilterComponent,
    LinkAddComponent
  ],
  templateUrl: './link.component.html',
  styleUrl: './link.component.css'
})
export class LinkComponent implements OnInit {
  pageSize: number = 10;
  currentPage: number = 1;
  totalPages: number = 10;
  totalLinkCount: number = 0;
  sizes: number[] = [3, 5, 10];
  pages: number[] = [];
  isAscending: boolean = true;
  arrowUp = ArrowUp;
  arrowDown = ArrowDown;
  chevronLeft = ChevronLeft;
  chevronRight = ChevronRight;
  x= X;
  plus = Plus;

  filterText: string = '';
  
  links: Link[] = [];

  openAddForm: boolean = false;

  constructor(private linkService: LinkService) {}

  ngOnInit(): void {
    this.getLinksByPagination();
  }

  getLinksByPagination() {
    let pageRequestDto: PageRequestDto = {
      pageNumber: this.currentPage,
      pageSize: this.pageSize,
      filterText: this.filterText
    };

    this.linkService
      .getLinksByPagination(pageRequestDto)
      .subscribe(response => {
        this.links = response.data.links;
        this.totalLinkCount = response.data.linkCount;
        this.totalPages = Math.ceil(this.totalLinkCount / this.pageSize);
        this.pages = [];
        for (let i = 1; i <= this.totalPages; i++) {
          this.pages.push(i);
        }
      });
  }

  prevPage() {
    if (this.currentPage > 1) {
      this.currentPage--;
      this.getLinksByPagination();
    }
  }

  nextPage() {
    if (this.currentPage < this.totalPages) {
      this.currentPage++;
      this.getLinksByPagination();
    }
  }

  reverse() {
    this.links = this.links.reverse();
    this.isAscending = !this.isAscending;
  }

  onSizeChange(size: number) {
    this.pageSize = size;
    this.getLinksByPagination();
  }

  onFilterChanged(text: string) {
    this.filterText = text;
    this.currentPage = 1;
    this.getLinksByPagination();
  }

  showAddForm() {
    this.openAddForm = true;
  }

  closeAddForm() {
    this.openAddForm = false;
  }
}
