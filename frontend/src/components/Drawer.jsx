import React, { useState } from "react";
import Drawer from "./Drawer";

export default function Header() {
  const [isDrawerOpen, setIsDrawerOpen] = useState(false);

  return (
    <header>
      <button onClick={() => setIsDrawerOpen(true)}>Open Menu</button>
      <Drawer isOpen={isDrawerOpen} onClose={() => setIsDrawerOpen(false)}>
        <nav>
          <a href="/activity">Activity</a>
          <a href="/lists">Watchlists</a>
          <a href="/profile">Profile</a>
        </nav>
      </Drawer>
    </header>
  );
}
