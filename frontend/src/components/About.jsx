import React, { useEffect, useRef } from "react";
import {
  Eye,
  Heart,
  AlignJustify,
  Star,
  Calendar,
  Bookmark,
} from "lucide-react";
import "../styles/About.css";

const About = () => {
  const featureRefs = useRef([]);

  useEffect(() => {
    const observer = new IntersectionObserver(
      (entries) => {
        entries.forEach((entry) => {
          if (entry.isIntersecting) {
            entry.target.classList.add("about-card-visible");
          }
        });
      },
      { threshold: 0.1 }
    );

    featureRefs.current.forEach((ref) => {
      if (ref) observer.observe(ref);
    });

    return () => {
      featureRefs.current.forEach((ref) => {
        if (ref) observer.unobserve(ref);
      });
    };
  }, []);

  const features = [
    {
      icon: <Eye strokeWidth={1.5} />,
      text: "Keep track of every film you've ever watched (or just start from the day you join)",
    },
    {
      icon: <Heart strokeWidth={1.5} />,
      text: "Show some love for your favorite films with a ❤️",
    },
    {
      icon: <AlignJustify strokeWidth={1.5} />,
      text: "Write reviews, and follow friends and other members to read theirs",
    },
    {
      icon: <Star strokeWidth={1.5} />,
      text: "Rate each film on a five-star scale to record and share your reaction",
    },
    {
      icon: <Calendar strokeWidth={1.5} />,
      text: "Keep a diary of your film watching",
    },
    {
      icon: <Bookmark strokeWidth={1.5} />,
      text: "Compile and share lists of films on any topic and keep a watchlist of films to see",
    },
  ];

  return (
    <div className="about-container">
      <div className="about-content">
        <div className="about-title-wrapper">
          <h2 className="about-title">LISTIFYCINEMA LETS YOU...</h2>
        </div>
        <div className="about-grid">
          {features.map((feature, index) => (
            <div
              key={index}
              className="about-card"
              ref={(el) => (featureRefs.current[index] = el)}
            >
              <div className="about-icon-wrapper">
                <div className="about-icon">{feature.icon}</div>
              </div>
              <p className="about-text">{feature.text}</p>
            </div>
          ))}
        </div>
      </div>
    </div>
  );
};

export default About;
