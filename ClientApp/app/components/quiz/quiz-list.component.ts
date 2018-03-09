﻿import { Component, Input, OnInit, Inject } from "@angular/core";
import { Router } from "@angular/router";
import { HttpClient } from "@angular/common/http";

@Component({
    selector: "quiz-list",
    templateUrl: "./quiz-list.component.html",
    styleUrls: ['./quiz-list.component.css']
})

export class QuizListComponent implements OnInit {
    @Input() class : string;
    title: string;
    selectedQuiz: Quiz;
    quizzes: Quiz[];

    constructor(private http: HttpClient,
        @Inject('BASE_URL') private baseUrl: string,
        private router: Router) {
        this.title = "Latest Quizzes";
        this.baseUrl = baseUrl;
    }

    ngOnInit(): void {
        console.log("QuizListComponent instantiated with the following class: " + this.class);

        var url = this.baseUrl + "api/quiz/";

        switch (this.class) {
            case "latest":
            default:
                this.title = "Latest Quizzes";
                url += "Latest/";
                break;

            case "byTitle":
                this.title = "Quizzes By Title";
                url += "ByTitle/";
                break;

            case "random":
                this.title = "Random Quizzes";
                url += "Random/";
                break;
        }

        this.http.get<Quiz[]>(url).subscribe(result => {
                this.quizzes = result;
            },
            error => console.error(error));
    }

    onSelect(quiz: Quiz) {
        this.selectedQuiz = quiz;
        console.log("Quiz with Id " + this.selectedQuiz.Id + " has been selected");
        this.router.navigate(["quiz", this.selectedQuiz.Id]);
    }
}