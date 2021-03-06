﻿import { Component, Inject, OnInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { HttpClient } from "@angular/common/http";

@Component({
    selector: "question-edit",
    templateUrl: "./question-edit.component.html",
    styleUrls: ["./question-edit.component.css"]
})

export class QuestionEditComponent {
    title: string;
    question: Question;

    editMode: boolean;

    constructor(private activatedRoute: ActivatedRoute,
        private router: Router,
        private http: HttpClient,
        @Inject("BASE_URL") private baseUrl: string) {

        // Empty question and will keep empty if creating a new question
        this.question = <Question>{};

        var id = +this.activatedRoute.snapshot.params["id"];

        this.editMode = (this.activatedRoute.snapshot.url[1].path === "edit");

        if (this.editMode) {
            // fetch
            var url = this.baseUrl + "api/question/" + id;
            this.http.get<Question>(url).subscribe(res => {
                    this.question = res;
                    this.title = "Edit - " + this.question.Text;
                },
                error => console.error(error));
        } else {
            this.question.QuizId = id;
            this.title = "Create a new question";
        }
    }

    onSubmit(question: Question) {
        var url = this.baseUrl + "api/question";

        if (this.editMode) {
            this.http.post<Question>(url, question).subscribe(res => {
                    var v = res;
                    console.log("Question " + v.Id + " has been updated");
                    this.router.navigate(["quiz/edit", v.QuizId]);
                },
                error => console.error(error));
        } else {
            this.http.put<Question>(url, question).subscribe(res => {
                var v = res;
                console.log("Question " + v.Id + " has been created");
                this.router.navigate(["quiz/edit", v.QuizId]);
            })
        }
    }

    onBack() {
        this.router.navigate(["quiz/edit", this.question.QuizId]);
    }
}