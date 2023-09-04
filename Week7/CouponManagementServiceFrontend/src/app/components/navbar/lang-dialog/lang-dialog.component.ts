import { Component } from '@angular/core';

@Component({
  selector: 'app-lang-dialog',
  templateUrl: './lang-dialog.component.html',
  styleUrls: ['./lang-dialog.component.css']
})
export class LangDialogComponent {
  languages = [
    { code: "en-US", name: "English", image: "../../../assets/images/Flag_of_the_United_States.png"},
    { code: "tr", name: "Türkçe", image: "../../../assets/images/Flag_of_Turkey.png"}
  ];
}
