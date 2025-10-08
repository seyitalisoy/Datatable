import { Component, OnInit } from '@angular/core';
import { LinkService } from '../../services/link.service';
import { Link } from '../../models/link';
import { CommonModule } from '@angular/common';
import { LucideAngularModule, ArrowDown, ArrowUp, ChevronLeft, ChevronRight, Plus, X, Trash2, LoaderIcon } from 'lucide-angular';
import { PageRequestDto } from '../../models/pageRequestDto';
import { FormsModule } from '@angular/forms';
import { FilterComponent } from '../filter.component/filter.component';
import { LinkAddComponent } from '../link-add.component/link-add.component';
import { LinkUpdateComponent } from '../link-update.component/link-update.component';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-link',
  imports: [
    CommonModule,
    LucideAngularModule,
    FormsModule,
    FilterComponent,
    LinkAddComponent,
    LinkUpdateComponent
  ],
  templateUrl: './link.component.html',
  styleUrl: './link.component.css'
})
export class LinkComponent implements OnInit {
  pageSize: number = 5;
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
  x = X;
  plus = Plus;
  deleteIcon = Trash2;
  loader = LoaderIcon;

  deletedLink!: Link;
  isLinksLoaded: boolean = false;

  filterText: string = '';
  currentLink!: Link;

  links: Link[] = [];

  openAddForm: boolean = false;
  openUpdateForm: boolean = false;
  openDeleteForm: boolean = false;

  constructor(private linkService: LinkService, private matSnackBar: MatSnackBar) { }

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
        this.isLinksLoaded= false;
        this.links = response.data.links;
        this.totalLinkCount = response.data.linkCount;
        this.totalPages = Math.ceil(this.totalLinkCount / this.pageSize);
        this.pages = [];
        for (let i = 1; i <= this.totalPages; i++) {
          this.pages.push(i);
        }
        this.isLinksLoaded = true;
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
    this.currentPage = 1;
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

  showUpdateForm(link: Link) {
    this.openUpdateForm = true;
    this.currentLink = link;
  }

  closeUpdateForm() {
    this.openUpdateForm = false;
  }

  showDeleteForm(event: MouseEvent, link: Link) {
    event.stopPropagation();
    this.deletedLink = link;
    this.openDeleteForm = true;
  }

  closeDeleteForm() {
    this.openDeleteForm = false;
  }

  deleteLink() {
    let link = this.deletedLink;
    this.linkService.delete(link).subscribe({
      next: (response) => {
        this.deletedLink = {} as Link;
        this.currentPage = 1;
        this.matSnackBar.open("Link Başarıyla silindi", "Kapat", {
          duration: 3000,
          panelClass: ["bg-green-600", "text-white"]
        });
        this.closeDeleteForm();
        this.getLinksByPagination();
      },
      error: (error) => {
        this.matSnackBar.open("Link silme başarısız", "Kapat", {
          duration: 3000,
          panelClass: ["bg-red-600", "text-white"]
        });
      }
    })
  }

}
