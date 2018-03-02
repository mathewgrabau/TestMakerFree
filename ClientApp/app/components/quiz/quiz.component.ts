import { Component, Input, Inject, OnInit } from '@angular/core';

@Component({
    selector: "quiz",
    templateUrl: "./quiz.component.html",
    styleUrls: ["./quiz.component.css"]
})

export class QuizComponent {
    @Input() quiz: Quiz;
}
