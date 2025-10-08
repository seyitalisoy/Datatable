import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import { LinkService } from '../../services/link.service';
import { Link } from '../../models/link';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-link-update',
  imports: [ReactiveFormsModule, FormsModule, CommonModule],
  templateUrl: './link-update.component.html',
  styleUrl: './link-update.component.css'
})
export class LinkUpdateComponent implements OnChanges {

  constructor(private linkService: LinkService
    , private formBuilder: FormBuilder
    , private matSnackBar: MatSnackBar) { }

  @Input() currentLink!: Link;

  linkUpdateForm!: FormGroup;

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['currentLink'] && this.currentLink) {
      this.createLinkUpdateForm(this.currentLink);
    }
  }

  createLinkUpdateForm(link: Link) {
    this.linkUpdateForm = this.formBuilder.group({
      url: [
        link.url,
        [
          Validators.required,
          Validators.pattern(
            /^(https?:\/\/)([\w\-@]+\.)+[\w\-@]+(\/[\w\-._~:/?#[\]@!$&'()*+,;=]*)?$/
          )

        ]
      ],
      name: [
        link.name,
        [
          Validators.required,
          Validators.minLength(2)
        ]
      ],
      description: [
        link.description
      ]
    })
  }

  updateLink() {
    if (this.linkUpdateForm.valid) {
      let updatedLink: Link = Object.assign({}, this.linkUpdateForm.value);
      updatedLink.id = this.currentLink.id;
      updatedLink.createdAt = this.currentLink.createdAt;
      updatedLink.updatedAt = this.currentLink.updatedAt,
        this.linkService.update(updatedLink).subscribe({
          next: (response) => {
            this.matSnackBar.open(response.message || "Link güncellendi", "Kapat", {
              duration: 3000,
              panelClass: ['bg-green-600', 'text-white']
            });
          },
          error: (error) => {
            this.matSnackBar.open("Link güncelleme başarısız", "Kapat", {
              duration: 3000,
              panelClass: ["bg-red-600", "text-white"]
            });
          }
        });
    } else {
      this.linkUpdateForm.markAllAsTouched();
      this.matSnackBar.open("Veriler güncellenirken hata oluştu", "Kapat", {
        duration: 3000,
        panelClass: ["bg-yellow-600", "text-black"]
      });
    }
  }
}
