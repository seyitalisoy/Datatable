import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { LinkService } from '../../services/link.service';
import { Link } from '../../models/link';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-link-add',
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
  templateUrl: './link-add.component.html',
  styleUrl: './link-add.component.css'
})
export class LinkAddComponent implements OnInit {

  linkAddForm!: FormGroup;

  constructor(private linkService: LinkService, 
    private formBuilder: FormBuilder,
    private snackBar : MatSnackBar) { }

  ngOnInit(): void {
    this.createLinkAddForm();
  }

createLinkAddForm() {
  this.linkAddForm = this.formBuilder.group({
    url: [
      '',
      [
        Validators.required,
        Validators.pattern(
          /^(https?:\/\/)([\w\-]+\.)+[\w\-]+(\/[\w\-._~:/?#[\]@!$&'()*+,;=]*)?$/
        )
      ]
    ],
    name: [
      '',
      [
        Validators.required,
        Validators.minLength(2)
      ]
    ],
    description: ['']
  });
}



  addLink() {
    if (this.linkAddForm.valid) {
      let link: Link = Object.assign({},this.linkAddForm.value);
      this.linkService.add(link).subscribe({
         next: (response) => {
          this.snackBar.open(response.message || 'Link eklendi', 'Kapat', {
            duration: 3000,
            panelClass: ['bg-green-600', 'text-white']
          });
        }
        , error: () => {
          this.snackBar.open('Link eklenemedi, hata oluştu', 'Kapat', {
            duration: 3000,
            panelClass: ['bg-red-600', 'text-white']
          });
        }
      });
    }else {
      this.linkAddForm.markAllAsTouched();
      this.snackBar.open('Form geçersiz', 'Kapat', {
        duration: 2000,
        panelClass: ['bg-yellow-500', 'text-black']
      });
    }

  }
}
