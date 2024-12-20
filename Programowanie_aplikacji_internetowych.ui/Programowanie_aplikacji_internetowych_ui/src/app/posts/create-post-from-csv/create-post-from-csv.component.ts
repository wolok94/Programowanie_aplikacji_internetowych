import { Component } from '@angular/core';
import { PostModule } from '../modules/post/post.module';
import { PostService } from '../services/post.service';
import { MessageService } from '../../shared/services/message.service';

@Component({
  selector: 'app-create-post-from-csv',
  standalone: true,
  imports: [PostModule],
  templateUrl: './create-post-from-csv.component.html',
  styleUrl: './create-post-from-csv.component.css'
})
export class CreatePostFromCsvComponent {
  selectedFile: File | null = null;
  isLoading: boolean = false;
  errorMessage: string | null = null;
  successMessage: string | null = null;

  constructor(private postService: PostService, private messageService : MessageService) {}

  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length > 0) {
      this.selectedFile = input.files[0];
      console.log('Wybrano plik:', this.selectedFile);
    }
  }

  onSubmit(): void {
    if (!this.selectedFile) {
      this.errorMessage = 'Musisz wybrać plik!';
      return;
    }

    this.isLoading = true;
    this.errorMessage = null;
    this.successMessage = null;

    const formData = new FormData();
    formData.append('file', this.selectedFile, this.selectedFile.name);

    console.log('FormData zawiera:', formData);

    this.postService.createPostFromCsv(formData).subscribe({
      next: () => {
        this.messageService.showMessage("Pomyślnie dodano posty", "success");
      }, error: (err) => {
        this.messageService.showMessage("Wystąpił błąd podczas dodawania postów. Spróbuj ponownie.", "error");
      }

    });
  }
}
