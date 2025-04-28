import React from "react";
import { Facebook, Instagram, Twitter, Mail, Youtube } from "lucide-react";
import "../styles/Footer.css";

export default function Footer() {
  return (
    <footer className="footer">
      <div className="footer-content">
        <h2>Contact Us</h2>
        <div className="social-icons">
          <a
            href="https://www.instagram.com/szedd_ki_takony/"
            className="icon instagram"
          >
            <Instagram />
          </a>
          <p>Bigelisz Dávid Ottovics</p>
          <a
            href="https://www.instagram.com/fknrbrt/"
            className="icon instagram"
          >
            <Instagram />
          </a>
          <p>Foki Norbert</p>
          <a
            href="https://www.instagram.com/rubentoldi/"
            className="icon instagram"
          >
            <Instagram />
          </a>
          <p>Toldi Ruben</p>
        </div>
      </div>
      <div className="footer-bottom">
        <p>Copyright &copy; 2025 All rights reserved</p>
      </div>
    </footer>
  );
}
