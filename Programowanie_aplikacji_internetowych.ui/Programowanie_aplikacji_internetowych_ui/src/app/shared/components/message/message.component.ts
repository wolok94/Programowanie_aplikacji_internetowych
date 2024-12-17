import { Component, Input, OnInit } from '@angular/core';
import { SharedModule } from '../../modules/shared/shared.module';
import { MessageService } from '../../services/message.service';

@Component({
  selector: 'app-message',
  standalone: true,
  imports: [SharedModule],
  templateUrl: './message.component.html',
  styleUrl: './message.component.css'
})
export class MessageComponent implements OnInit {
  message: string = '';
  timeoutId: any;
  type: string = '';
  constructor(private messageService : MessageService) {

  }
  ngOnInit(): void {
    this.messageService.successMessage.subscribe(res => {
      if (this.timeoutId) {
        clearTimeout(this.timeoutId);
      }
      this.message = res != undefined ? res?.message : '';
      this.type = res != undefined ? res?.type : '';
      this.timeoutId = setTimeout(() => {
        this.message = '';
      }, 3000);
    })

  }



}
