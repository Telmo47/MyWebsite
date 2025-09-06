import React from "react";
import ReactDOM from "react-dom/client";
import { Card, Button, Container, Row, Col } from "react-bootstrap";

const projects = [
    {
        title: "GGData",
        description: "My first website ever developed. A simple platform where users can review games selected by the admin.",
        link: "#"
    },
    {
        title: "AI Chat Assistant",
        description: "An AI-powered assistant built with C# and React, capable of real-time Q&A.",
        link: "#"
    },
    {
        title: "E-Commerce Store",
        description: "A modern online store built with ASP.NET Core + React frontend.",
        link: "#"
    }
];

function Portfolio() {
    return (
        <Container className="py-5">
            <h1 className="text-center mb-5">My Projects</h1>
            <Row>
                {projects.map((p, i) => (
                    <Col md={4} className="mb-4" key={i}>
                        <Card className="shadow-lg h-100">
                            <Card.Body>
                                <Card.Title>{p.title}</Card.Title>
                                <Card.Text>{p.description}</Card.Text>
                                <Button variant="primary" href={p.link}>View Project</Button>
                            </Card.Body>
                        </Card>
                    </Col>
                ))}
            </Row>
        </Container>
    );
}

// Render React component into div#portfolio-root
const root = ReactDOM.createRoot(document.getElementById("portfolio-root"));
root.render(<Portfolio />);
